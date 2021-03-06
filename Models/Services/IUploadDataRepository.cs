﻿using System;

namespace WebApplication.Models.Services
{
    public interface IUploadDataRepository
    {
        UploadData Get(Guid id);

        UploadData Add(UploadData uploadData);

        void Delete(Guid id);
    }
}