function VerificaParcelamento(obj) 
{
	if (obj.value=='') return 
	var tblParcelamento = document.getElementById('tblParcelamento');
	var txtParcela = document.getElementById('txtParcelas_int');
	var txtData = document.getElementById('txtData_data');
	var tdValor = document.getElementById('tdValor');
	if (obj.value=='aprazo') 
	{
		OcultarObjeto(tblParcelamento,false);
		txtParcela.focus();	
	}
	else
	{
		OcultarObjeto(tblParcelamento,true);
		txtData.focus();
	}
	if (obj.value =='avista' || obj.value =='boletobancario')
		OcultarObjeto(tdValor, true);
	else
		OcultarObjeto(tdValor, false);
}
function ChangeModelo(obj)
{
	var tblModelPeriodo = document.getElementById('tblModelPeriodo');
	var txtDataInicial = document.getElementById('txtModelDtaInicial_DATA');
	var txtDescricaoOS = document.getElementById('txtDescricaoOS_tud');
	var cbNovoModelo = document.getElementById('cbNovoModelo');
	if (obj.value=='0')
	{
		OcultarObjeto(tblModelPeriodo,true);
		txtDescricaoOS.focus();
	}
	else
	{
		if(!cbNovoModelo.checked)
		{
			OcultarObjeto(tblModelPeriodo,false);
			txtDataInicial.focus();
		}
	}
		
}
function OcultarObjeto(obj, oculta)
{
	if (oculta)
	{
		with (obj.style)
		{
			display = "none";
			disabled = false;
		}
	}
	else
	{
		with (obj.style)
		{
			display = "block";
			disabled = true;
		}
	}
}
function CalculaPercentual(obj, tipo)
{
	var valor = obj.value;
	var valorTotal = document.getElementById('txtValorTotal').value;
	var txtDesconto = document.getElementById('txtDesconto_moeda');
	var txtValorPagar = document.getElementById('txtValorPagar_moeda');
	var percentual = 0;

	valor = valor.replace(',','.');
	if (valorTotal == '')
		valorTotal = 0;
	valorTotal = parseFloat(valorTotal);
//Verifica o tipo se for tipo 0, então tera que calcular o percentual de
//desconto.	
	if (tipo == 0)
	{
		percentual = (1 - valor / valorTotal)*100;
		if (valorTotal == 0)
			percentual = 0;
		txtDesconto.value = percentual.toString().replace('.',',');
		FormataMoeda(txtDesconto);
	}
	else
	{
		percentual = parseFloat(valor);
		valorcalculado = valorTotal -(valorTotal * percentual/100);
		txtValorPagar.value = valorcalculado.toString().replace('.',',');
		FormataMoeda(txtValorPagar);
	}
}

function GerarImpressaoOrc()
{
	//Radion Button List de tipos de impressão.
	var rbl_p1 = "rblOrcImpressao"; 
	var rbl = "";
	var rblObjeto;
	var rblValor;
	var target = "";
	
	//Verifica todos os chekBox para definir qual o tipo de relatório.
	for(a = 0;a <= 4;a++)
	{
		rbl = rbl_p1 + "_" + a.toString();
		rblObjeto = document.getElementById(rbl);
		if (rblObjeto.checked)
			rblValor = rblObjeto.value;
	}
	//Atraves do rblValor será verificado qual relatório será chamado.
	switch(rblValor)
	{
		case "4":
		AbreRelatorio(rblValor,"_self");
		break;
		default:
		AbreRelatorio(rblValor,"_blank");
		break;
	}	
	function AbreRelatorio(tipo, target)	
	{
		if (tipo=="4")
		{
			window.open("../Aspx/Orc_gerador_relatorio.aspx",target);
			return;
		}
			window.open("../relatorios/orc_relatorios.aspx?tipo=" + tipo,target,"location=no,menubar=yes,resizable=yes,scrollbars=yes,status=no,toolbar=no,width=800,height=600");
	}
}