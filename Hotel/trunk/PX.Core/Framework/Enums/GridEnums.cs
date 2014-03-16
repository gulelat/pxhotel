namespace PX.Core.Framework.Enums
{
    public enum GroupOptionEnums
    {
        And = 1,
        Or = 2
    }

    public enum GroupOp
    {
        AND,
        OR
    }

    public enum Operations
    {
        Eq, // "equal"
        Ne, // "not equal"
        Lt, // "less"
        Le, // "less or equal"
        Gt, // "greater"
        Ge, // "greater or equal"
        Bw, // "begins with"
        Bn, // "does not begin with"
        Ew, // "ends with"
        En, // "does not end with"
        Cn, // "contains"
        Nc,  // "does not contain"
        In,//in, // "in"
        Ni // "not in"
    }

    public enum GridOperationEnums
    {
        Edit = 1,
        Add = 2,
        Del = 3
    }

    public enum GridSortEnums
    {
        Asc = 1,
        Desc = 2
    }
}
