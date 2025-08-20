using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.Net;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Description;

public static class WebServiceHelper
{
    private static string GetClassName(string url)
    {
        string[] parts = url.Split('/');
        string[] pps = parts[parts.Length - 1].Split('.');
        return pps[0];
    }
}

