using jsreport.Shared;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;

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
		
		return File(report.Content, "application/pdf", "file.pdf");
	}
}