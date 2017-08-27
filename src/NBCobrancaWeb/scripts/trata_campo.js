/****************************
 Tratamento de Campos v1.0
 Escrito por Éder Loch					
 ederloch@hotmail.com 
****************************/


function moeda(obj, key) {
	if((!inteiro(key)) && key!=44) return false // Código 44 da tabela acsii é a vírgula

	if (obj.value=='' && (key==46 || key==44)) {
		obj.value = '0,';
		return false
	}

	var r = new RegExp;
	r = /(,)/g;
	if (r.exec(obj.value)) { 
		if (key==44) return false
	}
}

function mascara(objForm, evtKeyPress, sMask) {
	evtKeyPress = (evtKeyPress) ? evtKeyPress : ((window.event) ? window.event : null);		
	var nTecla = (evtKeyPress.which) ? evtKeyPress.which : evtKeyPress.keyCode;	

	objForm.maxLength = sMask.length;
	sValue = objForm.value;
	//Implementação para responder com a data do dia caso as teclas H ou h sejam presionadas
	//só é válido para mascara de data.
	if (sMask=='##/##/####' && (nTecla == 104 || nTecla == 72))
	{
		var now = new Date();
		var dd = now.getUTCDate() + "/";
		var mm = (now.getUTCMonth() + 1) + "/";
		var yy = now.getUTCFullYear(); 
		
		if (dd.length < 3)
			dd = '0' + dd;
		
		if (mm.length < 3)
			mm = '0' + mm;
		
		today = dd + mm + yy;
		objForm.value = today
		return false;
	}
	if (nTecla<48 || nTecla>57 || nTecla==46) {return false}

	switch (sMask.charAt(sValue.length)) {
		case '.' : objForm.value += '.'; break;
		case ':' : objForm.value += ':'; break;
		case '-' : objForm.value += '-'; break;
		case '/' : objForm.value += '/'; break;
		case ')' : objForm.value += ')'; break;
		case '(' : objForm.value += '('; break;
	}
}

function fone(obj,key) {
	if(!inteiro(key) && key!=45) return false
	var valor = obj.value;
	if (valor.indexOf('-')!=-1 && key==45) return false
	var r = RegExp(/-/g);
	if (r.exec(obj.value)) {
		obj.maxLength = 11;
		//if (key==45) return false;
	}else
		obj.maxLength = 10;
}

//Aceita todos caracteres exeto números
function strings(key) {
	if ((key<48) || (key>57)) {return true} else {return false}
}
//Aceita apenas números
function inteiro(key) {
	if (((key>47) && (key<58)) || (key==46)) {return true} else {return false}
}
//Aceita apenas números, letras minusculas e maiusculas
function letrasNumeros(key) {
	if (((key>47) && (key<58)) || ((key>64) && (key<91)) || ((key>96) && (key<123))) {return true} else {return false}
}

function email(elem,key) {
	if (letrasNumeros(key) || key==45 || key==46 || key==95 || key==64) {return true} else {return false}
}


//===================================================//



function tipos(t,key,objeto,evento) {
	switch (t) {
		//Genéricos
		case 'STR'  : return strings(key);									break;
		case 'INT'  : return inteiro(key);									break;
		case 'LN'	: return letrasNumeros(key);							break;
		case 'TUD'  : return true; 											break; //Aceita qualquer caracter
		//Máscaras		
		case 'CPF'  : return mascara(objeto,evento,'###.###.###-##');		break;
		case 'CNPJ' : return mascara(objeto,evento,'##.###.###/####-##');	break;
		case 'CEP'  : return mascara(objeto,evento,'#####-###');			break;
		case 'DATA' : return mascara(objeto,evento,'##/##/####');			break;
		case 'HORA' : return mascara(objeto,evento,'##:##');
		//Pseudos
		case 'EMAIL': return email(objeto, key);							break;
		case 'MOEDA': return moeda(objeto, key);							break;
		case 'FONE' : return fone(objeto, key);								break;
		//default		: alert('O tipo de dado deste campo não é válido'); return true; break; //Caso o tipo não esteja definido aqui
	}
}



//===================================================//



function tipCampo(idCampo, objeto, evento, key) {
	var tamCampo = idCampo.length;
	//Se não for definido o tipo de dado no campo, retorna true, podendo adicionar qualquer coisa no campo
	if ((posTipoCampo = idCampo.lastIndexOf('_'))==-1) return true; 
	//Encontra o tipo
	var tipo = idCampo.substring(posTipoCampo+1, tamCampo);
	tipo = tipo.toUpperCase();
	return tipos(tipo, key, objeto, evento);
}



//===================================================//


//
// Abilita algumas teclas especiais para funcionar no FF
function cr(key) {
	switch (key) {
		case 46 : return true;	break; //Delete
		case 8  : return true;	break; //Backspace
		case 32 : return true;	break; //Barra de espaço
		case 37 : return true;	break; //Seta esquerda
		case 39 : return true;	break; //Seta direita
	}
}

// Alguns caracteres são bloqueados na qual o banco de dados SQLServer não aceita
function db(key) {
	switch (key) {
		//case 39 : return true;  break; //Chr ' Se travar, no firefox não vai funcionar a tecla seta pra direita
		case 91 : return true;  break; //Chr [
		case 93 : return true;  break; //Chr ]
		case 124: return true;  break; //Chr |
		default : return false; break;
	}
}

/*
 * Tabulação v1.0
 * Pressioando as teclas tab, enter, seta pra cima
 * faz tabulação
 */
function tabulacao(elem,key) {
	//Se estiver focado em um dos tipos abaixo então não faz tabulação
	switch (elem.type) {
		case 'textarea' : return false; break;
		case 'submit'	: return false; break;
		case 'button'	: return false; break;
		case 'reset'	: return false; break;
	}
	// faz tabulação se a tecla tab ou enter for pressionada
	if (elem.type!='select-one' && (key==9 || key==13)) {
		if (elem.form == undefined)
			return false;
		var i;
		var elementos = elem.form.elements.length; //Começa a contagem do 1
		for (i=0; i<elementos; i++)
			if (elem==elem.form.elements[i])
				break;
		i = (i + 1) % elementos;
		elem.form.elements[i].focus();
		return true; 
	}
}



document.onkeypress = function keypressEvent() {
	var e = arguments[0] || window.event;				// Captura o evento atual // var e = evt ? evt : event; //colocar evt na função
	if(!e) return	  									// Se 'e' não for um evento então retorna 'undefined' podendo escrever qualquer coisa no campo
	var elem = e.srcElement || e.target;				// Identifica o elemento do objeto form (input,textarea,select) / NS: e.target  IE: e.srcElement
	var key  = e.keyCode || e.which;					// Tecla pressionada, keyCode=IE which=NS,FF,OP

	if (db(key)) return false							// Bloqueia alguns caracteres que o banco de dados não aceita
	if (cr(key)) return true							// Abilita as teclas delete, backspace... Para o nav FF

	var tab = tabulacao(elem,key);						// Faz tabulação nos campos
	if (tab==false) return true							// Tecla tab pressionada ou se focado em outros tipos do form que não pode ser tabulado
	if (tab==true) return false							// 

	if (elem.type=='text') {							// Apenas nos campos text vamos fazer validações
		  return tipCampo(elem.id,elem,e,key);			// id, elemento do form, evento do form, tecla pressionada(ascii) 
	}else return
}





/*




*/
function acha(evt) {
	var evt = evt || window.event;				//Referenciar window.event p/ evt, esquema p/ IE
	var elem = evt.target || evt.srcElement;	//Identifica o elemento objeto (input), e.target p/ NS e e.srcElement p/ IE.
	return elem;
}

// 
//
function setFocus(elem) {
	var s = new String;							// variável local
	s = elem.id;								// id do campo
	document.getElementById(s).focus();			// foca o cursor campo
	document.getElementById(s).select();		// seleciona o valor do campo
	return false;
}

function isMail(evt) {
	var elem = acha(evt);
	if (elem.value=='') return
	var strMail = elem.value;
	var re = new RegExp;
	re = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
	var arr = re.exec(strMail);
	if (arr == null) {
		alert('E-mail inválido');
		elem.focus();
		elem.select();
		return false;
	}else return true
}

function isCPF(evt) {
	var elem = acha(evt);
	if (elem.value=='') return
	cpf = elem.value;
	cpf = cpf.replace('.','');
	cpf = cpf.replace('.','');
	cpf = cpf.replace('-','');
	
	erro = new String;
	//if (cpf.length < 11) erro += "Sao necessarios 11 digitos para verificacao do CPF! "; 
	//var nonNumbers = /\D/;
	//if (nonNumbers.test(cpf)) erro += "A verificacao de CPF suporta apenas numeros! "; 
	if (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999"){
			erro += "Numero de CPF invalido!"
	}
	var a = [];
	var b = new Number;
	var c = 11;
	for (i=0; i<11; i++){
		   a[i] = cpf.charAt(i);
		   if (i < 9) b += (a[i] * --c);
	}
	if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11-x }
	b = 0;
	c = 11;
	for (y=0; y<10; y++) b += (a[y] * c--); 
	if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11-x; }
	if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10])){
		   erro +="Digito verificador com problema!";
	}
	if (erro.length > 0){
		   alert(erro);
		   setFocus(elem);
		  //elem.setAttribute('autocomplete','off'); 
	}
	return true;
}
function isCNPJ(evt) {
		var elem = acha(evt);
		if (elem.value=='') return

		CNPJ = elem.value;
		erro = new String;
		//if (CNPJ.length < 18) erro += "É necessario preencher corretamente o número do CNPJ! "; 
		//if ((CNPJ.charAt(2) != ".") || (CNPJ.charAt(6) != ".") || (CNPJ.charAt(10) != "/") || (CNPJ.charAt(15) != "-")){
		//if (erro.length == 0) erro += "É necessário preencher corretamente o número do CNPJ! ";
		//}
		//substituir os caracteres que não são números
	   if(document.layers && parseInt(navigator.appVersion) == 4){
			   x = CNPJ.substring(0,2);
			   x += CNPJ. substring (3,6);
			   x += CNPJ. substring (7,10);
			   x += CNPJ. substring (11,15);
			   x += CNPJ. substring (16,18);
			   CNPJ = x; 
	   } else {
			   CNPJ = CNPJ. replace (".","");
			   CNPJ = CNPJ. replace (".","");
			   CNPJ = CNPJ. replace ("-","");
			   CNPJ = CNPJ. replace ("/","");
	   }
	   var nonNumbers = /\D/;
	   if (nonNumbers.test(CNPJ)) erro += "A verificação de CNPJ suporta apenas números! "; 
	   var a = [];
	   var b = new Number;
	   var c = [6,5,4,3,2,9,8,7,6,5,4,3,2];
	   for (i=0; i<12; i++){
			   a[i] = CNPJ.charAt(i);
			   b += a[i] * c[i+1];
	   }
	   if ((x = b % 11) < 2) { a[12] = 0 } else { a[12] = 11-x }
	   b = 0;
	   for (y=0; y<13; y++) {
			   b += (a[y] * c[y]); 
	   }
	   if ((x = b % 11) < 2) { a[13] = 0; } else { a[13] = 11-x; }
	   if ((CNPJ.charAt(12) != a[12]) || (CNPJ.charAt(13) != a[13])){
			   erro +="Dígito verificador com problema!";
	   }
	   if (erro.length > 0){
			   alert(erro);
			   elem.focus();
			   return false;
	   } else {
			   //alert("CNPJ valido!");
			   return true;
	   }
	   return true;
}
function isFone(evt) {
	var elem = acha(evt);
	if (elem.value=='') return
	var fone = elem.value;
	// Remove ponto e hifen
	var r = new RegExp;
	r = /(\.)|(-)/g;
	if (r.exec(elem.value)) { 
		fone = fone.replace(r,'');
	}
	// Não existe fones com menos de 7 caracteres
	if (fone.length<7) {
		elem.focus();
		elem.select();
		return false;
	}
	switch (fone.length) {
		case 7  : elem.value = (fone.substr(0,3))    + "-" + (fone.substr(3,7)); 	break;
		case 8  : elem.value = (fone.substring(0,4)) + "-" + (fone.substring(4,8)); break; 
		case 10 : elem.value = (fone.substring(0,4)) + "-" + (fone.substring(4,10)); 
	}
}
function isData(evt) {
   // return (/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/).test(this);
	var elem = acha(evt);
	if (elem.value=='') return
	var strMail = elem.value;
	var re = new RegExp;
	re = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
	var arr = re.exec(strMail);
	if (arr == null) {
		alert('Data inválida');
		elem.focus();
		elem.select();
		return false;
	}
}

function fMoeda() {
	var e = arguments[0] || window.event;				// Captura o evento atual // var e = evt ? evt : event; //colocar evt na função
	if(!e) return	  									// Se 'e' não for um evento então retorna 'undefined' podendo escrever qualquer coisa no campo
	var elem = e.srcElement || e.target;				// Identifica o elemento do objeto form (input,textarea,select) / NS: e.target  IE: e.srcElement
	FormataMoeda(elem);	
}

function FormataMoeda(elem) {
	
	var val = elem.value;
	var tam = val.length;
	var dec = ",00";
	
	var r = new RegExp;
	r = /(\.)/g;
	if (r.exec(elem.value)) { 
		ponto = val.lastIndexOf('.');
		val1 = elem.value.replace(r,'');
		tam = val1.length;
		if ((tam-ponto) == 2)
			val = elem.value.replace(r,',');
		else
			val = elem.value.replace(r,'');
	}
	
	if (val.indexOf(',') != -1) {
		v   = val.indexOf(',');
		dec = val.substring(v, v+3);
		val = val.substring(0, v);
		tam = val.length;
	}

	if (dec.length == 2)
		dec = dec + '0';

	switch ( tam ) {
		case 1 : elem.value =  val+dec;	break;
		case 2 : elem.value =  val+dec;	break;		
		case 3 : elem.value =  val+dec;	break;
		case 4 : elem.value =  val.charAt(0) + '.' + val.substr(1,3) + dec;		break;
		case 5 : elem.value =  val.substring(0,2) + '.' + val.substring(2,5) + dec;		break;
		case 6 : elem.value =  val.substring(0,3) + '.' + val.substring(3,6) + dec;		break;
		case 7 : elem.value =  val.charAt(0) + '.' + val.substring(1,4) + '.' + val.substring(4,7) + dec;		break;
		case 8 : elem.value =  val.substring(0,2) + '.' + val.substring(2,5) + '.' + val.substring(5,8) + dec;		break;
	}
}

window.onload = function() {
	var i = 0, t; 																	// variáveis local
	//document.forms[0].elements[0].focus();											// foca no primeiro objeto do form
	val = ['CPF		=	isCPF',														// Nome do tipo do campo e o nome da função
		   'CNPJ	=	isCNPJ',
		   'EMAIL	=	isMail',
		   'FONE	=	isFone',
		   'DATA	=	isData',
		   'MOEDA	=	fMoeda'];	
	rEspacos = new RegExp(/\s/g);
	for (t=0; t<val.length; t++) {
		val[t] = val[t].replace(rEspacos,'');
	}
	for (iCampo = 0; iCampo < document.forms[0].elements.length; iCampo++) {
		var tipo = document.forms[0].elements[iCampo].id.split('_', 2);
		if (tipo[1]) {
			tipoCampo = tipo[1].toUpperCase();
			while (i<val.length) {
				valDados = val[i].split('=',2);
				if (tipoCampo==valDados[0]) {
					document.forms[0].elements[iCampo].onblur = eval(valDados[1]);  // não tem como enviar parâmetros para a função. uiia!
					
					if (tipoCampo=='MOEDA') {
						document.forms[0].elements[iCampo].onfocus = function() {
							var evt = arguments[0] || window.event;
							var obj = evt.srcElement || evt.target;
							
							var r = new RegExp;
							r = /(\.)/g;
							if (r.exec(obj.value)) { 
								obj.value = obj.value.replace(r,'');
							}
							obj.select();
						}
					}
					
				}
				i++;
			}
			i = 0;
		}
	}
}