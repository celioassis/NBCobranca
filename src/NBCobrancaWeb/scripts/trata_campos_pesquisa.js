
function vCampoProcurar(obj, evt) {
	var evt = (evt) ? evt : event;
	var key = evt.keyCode || evt.which;

	switch (obj.value) {
		// Aceita todos caracteres exceto n�meros
		case '2' : case '4' : 
			var caract = new RegExp(/[^0-9]/); // Equivalente � /\D/ 
			break;
		// Aceita apenas n�meros
		case '1' : case '3' :
			var caract = new RegExp(/[0-9 .]/); // Equivalente � /\d/
			break;
		// Aceita qualquer caracter
		default : return; 
	}

	var caract = caract.test(String.fromCharCode(key));
	if (!caract) {
		//alert("invalido: " + String.fromCharCode(key));
		return false;
	} else 
		return true				
}
	
function limpatxtProcurar() {
	with (document.forms[0].elements["ctl00_phConteudo_txtProcurar"]) {
		value = '';
		//select();
		focus();
	}
    with (document.forms[0].elements["txtProcurar"]) {
		    value = '';
		    //select();
		    focus();
	    }
	
}