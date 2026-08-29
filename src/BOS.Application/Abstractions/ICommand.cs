namespace BOS.Application.Abstractions;

/// <summary>
/// CQRS - Command interface.
/// </summary>
public interface ICommand;

/// <summary>
/// CQRS - Command with result.
/// </summary>
public interface ICommand<TResult>;
