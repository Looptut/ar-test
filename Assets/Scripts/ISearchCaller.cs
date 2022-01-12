using System;

public interface ISearchCaller
{
    event Action OnStartSearching;
    event Action OnEndSearching;
}
