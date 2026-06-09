using Microsoft.JSInterop;
namespace StudentPanel.Client.Services;

public class ClipboardService(IJSRuntime js) : IAsyncDisposable
{
    private IJSObjectReference? _module;

    private async Task<IJSObjectReference> GetModuleAsync() =>
        _module ??= await js.InvokeAsync < IJSObjectReference > (
            "import", "./js/clipboard.js");

    public async Task CopyAsync(string text)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("copyToClipboard", text);
    }

    public async Task<bool> ConfirmRemoveAsync(string name)
    {
        var module = await GetModuleAsync();
        return await module.InvokeAsync<bool>("confirmRemove", name);
    }

    public async ValueTask DisposeAsync()
    {
        if (_module != null)
            await _module.DisposeAsync();
    }
}