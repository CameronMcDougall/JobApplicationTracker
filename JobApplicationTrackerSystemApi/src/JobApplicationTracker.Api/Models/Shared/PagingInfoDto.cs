﻿namespace JobApplicationTracker.Api.Models.Shared;

public class PagingInfoDto
{
    public int Current { get; set; }

    public int TotalPages { get; set; }

    public int TotalItems { get; set; }
}