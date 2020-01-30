﻿using System;

namespace WebApplication.Models.Services
{
    public interface IUploadDataRepository
    {
        UploadData Get(string id);

        UploadData Add(UploadData uploadData);

        void Delete(string id);
    }
}
