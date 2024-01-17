using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ZXingBlazor.Components;

namespace bh001_camera_barcode.Shared.Scanner
{
    public partial class Barcode
    {
        [Inject] public IJSRuntime Js { get; set; } = default!;
        /// <summary>
        /// Barcode Id (HTML)
        /// </summary>
        [Parameter] public string Id { get; set; } = "barcode-" + Guid.NewGuid();
        /// <summary>
        /// Barcode format. Default is CODE_128
        /// </summary>
        [Parameter] public BarcodeFormat BarcodeFormat { get; set; } = BarcodeFormat.CODE_128;
        /// <summary>
        /// Barcode Class (CSS)
        /// </summary>
        [Parameter] public string Class { get; set; }
        /// <summary>
        /// The value of the barcode to be displayed
        /// </summary>
        [Parameter] public string Value { get; set; } = string.Empty;
        [Parameter] public EventCallback<string> ValueChanged { get; set; }
        [Parameter] public int Width { get; set; } = 200;
        // 12:5 aspect ratio
        /// <summary>
        /// Barcode height to maintain 12:5 aspect ratio
        /// </summary>
        private int _height => (int)(Width * 0.4166666666666667f);

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Js.InvokeVoidAsync("modifySvgViewbox", Id);
        }
    }
}
