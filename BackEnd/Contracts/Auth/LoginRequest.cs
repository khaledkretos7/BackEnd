﻿namespace BackEnd.Contracts.Auth;

public record LoginRequest(
    string UserName,
    string Password
);