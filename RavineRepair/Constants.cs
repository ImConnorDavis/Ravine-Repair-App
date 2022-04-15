using System;
namespace RavineRepair
{
    public static class Constants
    {
        public static readonly string TenantName = "iphoenixdev";
        public static readonly string TenantId = "iphoenixdev.onmicrosoft.com";
        public static readonly string ClientId = "daa432ee-28a1-4d0c-8ad6-baacb6476085";
        public static readonly string SignInPolicy = "B2C_1_MainSignIn";
        public static readonly string IosKeychainSecurityGroups = "com.microsoft.adalcache";
        public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
        public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}";
        public static readonly string AuthoritySignIn = $"{AuthorityBase}/{SignInPolicy}";
    }
}
