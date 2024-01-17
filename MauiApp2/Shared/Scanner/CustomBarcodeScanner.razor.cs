using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using ZXingBlazor.Components;

namespace bh001_camera_barcode.Shared.Scanner
{
    public partial class CustomBarcodeScanner
    {
        [Inject] IDialogService DialogService { get; set; } = default!;
        [Inject] IJSRuntime Js { get; set; } = default!;
        [Parameter] public EventCallback<string> OnScanResult { get; set; }
        [Parameter] public EventCallback OnScanClick { get; set; }
        [Parameter] public EventCallback OnCloseClick { get; set; }
        [Parameter] public string Title { get; set; } = "Código de barras";
        [Parameter] public string Class { get; set; } = string.Empty;
        BarcodeReader Reader = new();

        public async Task GetError(string errorMessage)
        {
            await Task.Delay(0);
            if (errorMessage.Contains("OverconstrainedError"))
            {
                Reader.Start();
                return;
            }
            CameraError = true;
            if (errorMessage.Contains("Permission"))
            {
                ErrorMessage = "No se ha podido acceder a la cámara. Por favor, compruebe que tenga los permisos para acceder a la cámara.";
            }
            else
            {
                ErrorMessage = errorMessage;
            }
            StateHasChanged();
        }
        bool ShowScanBarcode { get; set; } = false;
        string ErrorMessage { get; set; } = "Ha ocurrido un error al intentar escanear el código de barras.";
        bool CameraError { get; set; } = false;

        private async void ScanResult(string barcode)
        {
            ShowScanBarcode = !ShowScanBarcode;
            CameraError = false;
            await OnScanResult.InvokeAsync(barcode);
        }

        private async Task HandleButtonClick()
        {
            ShowScanBarcode = true;
            await Js.InvokeVoidAsync("removeButtons");
            await OnScanClick.InvokeAsync();
        }

        private async Task HandleCloseClick()
        {
            ShowScanBarcode = false;
            await OnCloseClick.InvokeAsync();
            CameraError = false;
        }

        private async Task ShowManualInputDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            await DialogService.ShowAsync<ManualInputDialog>(
                Title,
                new DialogParameters { ["OnSubmit"] = EventCallback.Factory.Create(this, new Action<string>(async barcode => await OnScanResult.InvokeAsync(barcode))) },
                options
            );
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Js.InvokeVoidAsync("removeButtons");
        }
    }
}
