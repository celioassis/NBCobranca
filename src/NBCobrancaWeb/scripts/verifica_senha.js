
function senhaCaracteres(obj) {
	if (obj.value=='') return 
	if (obj.value.length<6) {
		alert("A senha deve ter no m�nimo 6 caracteres.");
		obj.focus();
		obj.select();
	}
}
function senhaConfirma(obj) {
	if (obj.value=='') return 
	objSenha = document.getElementById("txtSenha_TUD");
	if (obj.value!=objSenha.value) {
		alert("Suas senhas n�o correspondem. Digite novamente.");
		objSenha.focus();
		objSenha.select();
		obj.value = '';
	}
}