using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace bh001_camera_barcode.Shared.Scanner
{
    public partial class BarcodeScannerUI
    {
        [Parameter] public string Src { get; set; } = "/barcodebox.png";
        [Parameter] public string Title { get; set; } = "Escanee el código de barras";
        [Parameter] public string InputTitle { get; set; } = "Ingresar código de barras";
        [Parameter] public EventCallback<string> OnScanResult { get; set; }
        [Parameter] public bool HideOnScan { get; set; } = true;
        [Parameter] public MaxWidth MaxWidth { get; set; } = MaxWidth.Small;
        private bool DisplayBarcodeBox { get; set; } = true;

        private void HandleScanResult(string result)
        {
            DisplayBarcodeBox = true;
            OnScanResult.InvokeAsync(result);
        }
    }
}
