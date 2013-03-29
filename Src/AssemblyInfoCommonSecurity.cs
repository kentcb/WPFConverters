// SECURITY ATTRIBUTES
//   this is messy because different runtimes have different security models and therefore different attributes available
//   also, a bug in ATP prevents us from applying certain attributes for FX40 builds. See https://connect.microsoft.com/VisualStudio/feedback/details/777864/

#if !FX35 && !FX40 && !SL40 && !SL50
using System.Security;
#endif

#if FX35
using System.Security.Permissions;
#endif

#if !FX35 && !FX40 && !SL40 && !SL50
[assembly: SecurityRules(SecurityRuleSet.Level2)]
[assembly: SecurityTransparent]
[assembly: AllowPartiallyTrustedCallers]
#endif

#if FX35
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
#endif