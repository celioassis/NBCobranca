function ConfirmacaoComMotivo(mensagem, callback, usaInput, mensagemMotivo, hfMotivo, mensagemFaltaMotivo) {
    if (usaInput)
        mensagem += '<br/>' + mensagemMotivo + ': <input type="text" class="form-control">';

    if (mensagemFaltaMotivo == undefined || mensagemFaltaMotivo === '')
        mensagemFaltaMotivo = 'É preciso descrever um motivo para a ação que será executada.';

    new BootstrapDialog({
        title: 'Confirmação',
        message: mensagem,
        closable: false,
        data: {
            'callback': callback
        },
        buttons: [{
            label: 'OK',
            cssClass: 'btn-primary',
            action: function (dialog) {
                var motivo = dialog.getModalBody().find('input').val();
                if (motivo != '') {
                    hfMotivo.val(motivo);
                    typeof dialog.getData('callback') === 'function' && dialog.getData('callback')(true);
                    dialog.close();
                }
                else
                    BootstrapDialog.alert({
                        title: 'Mensagem do Sistema',
                        message: mensagemFaltaMotivo,
                        type: BootstrapDialog.TYPE_WARNING
                    });
            }
        }, {
            label: 'Cancelar',
            action: function (dialog) {
                typeof dialog.getData('callback') === 'function' && dialog.getData('callback')(false);
                dialog.close();
            }
        }]
    }).open();
};

var iframeCarregado = false;
var ModalViaBootStrip = true;

function carregouIframe() {
    setTimeout(function() {
        $('.modal-body').unblock();
    }, 200);
};

function onShowModal() {
    $('.modal-body').css('padding', 0);
    iframeCarregado = false;
    $('.modal-body').block({
        message: '<h3>Carregando Ficha ...</h3>',
        ignoreIfBlocked: true
    });
}

function ShowModal(titulo, pathAspx, altura) {
    BootstrapDialog.show({
        title: titulo,
        message: $("<iframe src='" + pathAspx + "' style='border: black; width: 100%; height: " + altura + "px;' onload='carregouIframe()'></iframe>"),
        size: BootstrapDialog.SIZE_WIDE,
        draggable: true,
        closeByBackdrop: false,
        closeByKeyboard: false,
        onshown: onShowModal
    });
}

/**
 * Fecha uma janela modal identificando se a mesma foi aberta via BootStrapDialog
 * @returns {} 
 */function FecharModal() {
    if (window.parent.ModalViaBootStrip)
        window.parent.$.each(window.parent.BootstrapDialog.dialogs, function (id, dialog) {
            dialog.close();
        });
    else
        window.parent.hidePopWin();
};
