using System;
using System.Runtime.Serialization;

namespace Movies.DomainLayer.Managers.Enums
{
    [Serializable]
    public enum Genre
    {
        [EnumMemberAttribute]
        NA,
        [EnumMemberAttribute]
        Action,
        [EnumMemberAttribute]
        Animation,
        [EnumMemberAttribute]
        Drama,
        [EnumMemberAttribute]
        Musical,
        [EnumMemberAttribute]
        SciFi,
        [EnumMemberAttribute]
        Thriller
    }
}
