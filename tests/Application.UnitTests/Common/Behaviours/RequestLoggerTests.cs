﻿using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateCustomerCommand>> _logger = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateCustomerCommand>>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateCustomerCommand>(_logger.Object);
        await requestLogger.Process(new CreateCustomerCommand { ListId = 1, Title = "title" }, new CancellationToken());
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateCustomerCommand>(_logger.Object);
        await requestLogger.Process(new CreateCustomerCommand { ListId = 1, Title = "title" }, new CancellationToken());
    }
}
