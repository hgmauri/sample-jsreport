using jsreport.Shared;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;
using Sample.JsReport.Extensions;

namespace Sample.JsReport.Controllers;

[ApiController]
[Route("[controller]")]
public class ConverterHtml : ControllerBase
{
	private readonly IRenderService _render;

	public ConverterHtml(IRenderService render)
	{
		_render = render;
	}

	[HttpPost]
	public async Task<IActionResult> Convert([FromForm] string html)
	{
		var report = await _render.RenderAsync(new RenderRequest
		{
			Template = new Template
			{
				Recipe = Recipe.ChromePdf,
				Engine = Engine.JsRender,
				Content = html
			}
		});

		var result = UtilExtensions.ReadToEnd(report.Content);
		
		return File(result, "application/pdf", "file.pdf");
	}
}