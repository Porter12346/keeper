global using System;
global using System.Data;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Web;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using System.Threading.Tasks;
global using Dapper;
global using CodeWorks.Utils;

// APPLICATION SPECIFIC
global using keeper.Repositories;
global using keeper.Services;
global using keeper.Models;
global using Amazon.Runtime;
global using Amazon.Runtime.CredentialManagement;
global using Amazon.S3;
global using Amazon.S3.Model;
global using Amazon.SecurityToken;
global using Amazon.SecurityToken.Model;