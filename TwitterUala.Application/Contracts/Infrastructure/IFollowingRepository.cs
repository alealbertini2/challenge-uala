﻿using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.Contracts.Infrastructure
{
    public interface IFollowingRepository
    {
        void FollowUser(Following follow);
        IEnumerable<Tweet> TweetsFromFollowingByUserId(long userId);
    }
}