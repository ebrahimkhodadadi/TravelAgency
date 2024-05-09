using NetArchTest.Rules;
using TravelAgency.Tests.Unit.ArchitectureTests.Utilities.CustomRules;

namespace TravelAgency.Tests.Unit.ArchitectureTests.Utilities;

public static class ArchTestUtilities
{
    public static ConditionList HavePrivateParameterlessConstructor(this Conditions conditions)
    {
        return conditions.MeetCustomRule(new HavePrivateParameterlessConstructor());
    }

    public static ConditionList DefineMethod(this Conditions conditions, string methodName)
    {
        return conditions.MeetCustomRule(new DefinesMethod(methodName));
    }
}