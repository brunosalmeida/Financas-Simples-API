﻿using FS.Data.Entities;

namespace FS.Data.Mappings
{
    public static class UserDomainToUserEntityMapper
    {
        public static User MapFrom(FS.Domain.Model.User model)
        {
            if (model is null) return null;

            return new User()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn,
                Password = model.Password
            };
        }
    }
}