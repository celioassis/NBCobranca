
	//Mostra e esconde a tag 'table' dentro os campos para
	//cadastro de pessoa física ou jurídica
	function TabelaFJMostraEsconde(mostra,esconde) {
		objMostra = document.getElementById(mostra);
		objEsconde = document.getElementById(esconde);
		with (objMostra.style) {
			display = "block";
			disabled = true;	
		}
		with (objEsconde.style) {
			display = "none";
			disabled = false;
		}				
	}
	
	//Esconde a tag 'span' que contém o campo 'quantidade' na
	//tela cadastro de itens
	function CampoQuantidadeMostra(mostra) {
		objMostra = document.getElementById(mostra);
		with (objMostra.style) {
			display = "block";
			disabled = true;
		}
	}
	function CampoQuantidadeEsconde(esconde) {
		objEsconde = document.getElementById(esconde);
		with (objEsconde.style) {
			display = "none";
			disabled = false;
		}
	}
	
	//Esconde a tag 'span' que contém o campo 'quantidade' e
	//a que contém o campo 'número de Série' tela cadastro de itens
	function CadItem_MostraEscondeCampo() {
		objRbl = document.getElementById('rblTipoItem_0');
			if (objRbl.checked)
			{
				CampoQuantidadeEsconde("spnQuantidade");
				CampoQuantidadeMostra("spnNumSerie");
			}
			else
			{
				CampoQuantidadeMostra("spnQuantidade");
				CampoQuantidadeEsconde("spnNumSerie");
			}
		
	}
	
	//Função para o gerenciamento de classes opção click Genérica
	//para todos os botões desde novo até renomear, ajuste do lado do cliente.
	function ClasseBtnClick(txtCampo, Acao, Mostra, Esconde)
	{
		objCampoTxt = document.getElementById(txtCampo);
		TabelaFJMostraEsconde(Mostra,Esconde)		
		if(Acao=='renomear')
		{
			var curNode = TreeView1.SelectedNode;
			objCampoTxt.value = curNode.Text;
			objCampoTxt.select();
		}
		else
			objCampoTxt.value = 'Digite o nome da pasta';


	}
	

	//Mostra e esconde o elemento 'div' que contém treeview (classes) na
	//tela cadastro de itens
	
	/*-------------------------------------------------------------------------*/
	// Hide / Unhide menu elements
	/*-------------------------------------------------------------------------*/

	function ShowHide(id1, id2)
	{
		if (id1 != '') toggleview(id1);
		if (id2 != '') toggleview(id2);
	}
		
	/*-------------------------------------------------------------------------*/
	// Get element by id
	/*-------------------------------------------------------------------------*/

	function my_getbyid(id)
	{
		itm = null;
		
		if (document.getElementById)
		{
			itm = document.getElementById(id);
		}
		else if (document.all)
		{
			itm = document.all[id];
		}
		else if (document.layers)
		{
			itm = document.layers[id];
		}
		
		return itm;
	}

	/*-------------------------------------------------------------------------*/
	// Show/hide toggle
	/*-------------------------------------------------------------------------*/

	function toggleview(id)
	{
		if ( ! id ) return;
		
		if ( itm = my_getbyid(id) )
		{
			if (itm.style.display == "none")
			{
				fadeIn(itm,1);
			}
			else
			{
				fadeOut(itm,1);
			}
		}
	}

	/*-------------------------------------------------------------------------*/
	// Set DIV ID to hide
	/*-------------------------------------------------------------------------*/

	function my_hide_div(itm)
	{
		if ( ! itm ) return;
		
		itm.style.display = "none";
	}

	/*-------------------------------------------------------------------------*/
	// Set DIV ID to show
	/*-------------------------------------------------------------------------*/

	function my_show_div(itm)
	{
		if ( ! itm ) return;
		
		itm.style.display = "";
	}


function fadeIn(element,time){
        	processa(element,time,0,100);
}



function fadeOut(element,time){
        	processa(element,time,100,0);
}



function processa(element,time,initial,end){
    if(initial == 0){
        increment = 10;
        element.style.display = "block";
    }else {
        increment = -10;
    }

    opc = initial;

    intervalo = setInterval(function(){
 	    if((opc == end)){
                  if(end == 0){
                        element.style.display = "none";
                  }
                  clearInterval(intervalo);
 	    }else {
                  opc += increment;
                  element.style.opacity = opc/100;
                  element.style.filter = "alpha(opacity="+opc+")";
 	    }
    },time * 10);
}