function VisualizaRelatorio(pNomeRelatorio, pTarget)	
{
	VisualizaRelatorio(pNomeRelatorio,pTarget,null);
}
function VisualizaRelatorio(pNomeRelatorio, pTarget, pParametro)
{
	if(pParametro != null)
		pNomeRelatorio += pParametro;
	window.open(pNomeRelatorio,pTarget,"location=no,menubar=yes,resizable=yes,scrollbars=yes,status=no,toolbar=no,width=800,height=600");
}
