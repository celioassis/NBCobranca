var gPopupMask = null;
    gPopupMask = document.getElementById("popupMask");
    initPopUp();
}
    if (gPopupIsShown == true) {
        if (width == null || isNaN(width))
    }

    gPopupIsShown = false;
}
    if (window.frames["popupFrame"].document.title == null)
}
    if (gPopupIsShown && e.keyCode == 9) return false;
}
    if (document.all) {
        var i = 0;
            var tagElements = document.getElementsByTagName(gTabbableTags[j]);
                gTabIndexes[i] = tagElements[k].tabIndex;
            }
        }
    }
}
    if (document.all) {
        var i = 0;
            var tagElements = document.getElementsByTagName(gTabbableTags[j]);
                tagElements[k].tabIndex = gTabIndexes[i];
            }
        }
    }
}
    for (var i = 0; i < document.forms.length; i++) {
        for (var e = 0; e < document.forms[i].length; e++) {
    }
}
    for (var i = 0; i < document.forms.length; i++) {
        for (var e = 0; e < document.forms[i].length; e++) {
            if (document.forms[i].elements[e].tagName == "SELECT")
        }
    }
}

/**
 * Fecha uma janela modal identificando se a mesma foi aberta via BootStrapDialog
 * @returns {} 
 */
    if (window.parent.ModalViaBootStrip)
        window.parent.$.each(window.parent.BootstrapDialog.dialogs, function (id, dialog) {
            dialog.close();
        });
    else
        hidePopWin();
}