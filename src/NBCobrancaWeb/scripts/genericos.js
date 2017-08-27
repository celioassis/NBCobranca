    
  //Abre popup
  function popup(url) {
    window.open(url,"","scrollbars=1,resizable=0,menubar=0,toolbar=0,personalbar=0,width=580,height=400,left=100,top=100");
  }
  function dialog(url) {
    caixa = "window.showModelessDialog(url, '', 'Resizable:no; DialogWidth:580px; DialogHeight:400px; Edge:raised; Help:no; Scroll:no; Status:no; Center:yes;');";
    eval(caixa);
  }  
  function modal(url) {
	window.showModalDialog(url,"","dialogHeight:400px; dialogWidth:580px; dialogTop:200px; dialogLeft:200px; edge:Sunken; center:Yes; help:No; resizable:No; status:No;");
  }

  /*
  //escrever na barra de status do IE 
  function TStatus()  {
  	window.status="www.neobridge.com.br"
  	setTimeout("TStatus()",100);
  }
  TStatus();

  //abre janela pop-up para imprimir
  function imprimir(url) {
    window.open(url,"","scrollbars=yes,menubar=yes,personalbar=no,width=640,height=400,left=70,top=100");
  }
  	
  hoje = new Date()
  dia = hoje.getDate()
  dias = hoje.getDay()
  mes = hoje.getMonth()
  ano = hoje.getYear()
  if (dia < 10)
  dia = "0" + dia
  if (ano < 2000)
  ano = "19" + ano
  function CriaArray (n) {
  this.length = n }
  NomeDia = new CriaArray(7)
  NomeDia[0] = "Domingo"
  NomeDia[1] = "Segunda-feira"
  NomeDia[2] = "Terça-feira"
  NomeDia[3] = "Quarta-feira"
  NomeDia[4] = "Quinta-feira"
  NomeDia[5] = "Sexta-feira"
  NomeDia[6] = "Sábado"
  NomeMes = new CriaArray(12)
  NomeMes[0] = "Janeiro"
  NomeMes[1] = "Fevereiro"
  NomeMes[2] = "Março"
  NomeMes[3] = "Abril"
  NomeMes[4] = "Maio"
  NomeMes[5] = "Junho"
  NomeMes[6] = "Julho"
  NomeMes[7] = "Agosto"
  NomeMes[8] = "Setembro"
  NomeMes[9] = "Outubro"
  NomeMes[10] = "Novembro"
  NomeMes[11] = "Dezembro"
	
	
<!--- INICIO Abre a página do serviços online pequena --->
  function Confirma() {
	if (document.frm_login.edit_matricula.value == "") {
	  alert("Digite a sua matrícula para Entrar!")
	}else{
	  pagina = 'http://www.neobridge.com.br/primeiro_acesso.asp?matricula=' + document.frm_login.edit_matricula.value
	  document.frm_login.edit_matricula.value = ''
	  window.open(pagina,"servicos_online","toolbar=1,location=0,directories=0,status=1,menubar=1,scrollbars=1,resizable=1,screenX=0,screenY=0,left=0,top=0,width=771,height=451")
    }
  }
	
  function OnClick() {
	if (event.keyCode == 13) event.returnValue = true
	else if (event.keyCode > 57) event.returnValue = false
	else if (event.keyCode < 48) event.returnValue = false
  }
<!--- FIM Abre a pádgina do serviços online pequena --->
*/