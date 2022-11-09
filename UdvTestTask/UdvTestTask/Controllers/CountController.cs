﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UdvTestTask.Abstractions;
using UdvTestTask.Data;
using UdvTestTask.Models;

namespace UdvTestTask.Controllers;

public class CountController : Controller
{
    private readonly ILetterCountService _countService;
    private readonly IPageService _pageService;
    private readonly IAccountService _accountService;
    private readonly ILogger<CountController> _logger;
    private readonly IRepository<LettersCount> _repository;

    public CountController(ILetterCountService countService, IPageService pageService, IAccountService accountService,
        ILogger<CountController> logger, IRepository<LettersCount> repository)
    {
        _countService = countService;
        _pageService = pageService;
        _accountService = accountService;
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<Dictionary<char, int>>> Count()
    {
        if (!_accountService.IsAuthorized())
            return Unauthorized();
        
        _logger.LogInformation($"Start letter counting");
        
        var posts = await _pageService.GetLastPosts();
        var result = _countService.Count(posts.ToArray());
        
        await _repository.AddAsync(new LettersCount()
        {
            LettersData = JsonSerializer.Serialize(result)
        });
        
        _logger.LogInformation($"Counting ends");
        return Ok(result);
    }
}