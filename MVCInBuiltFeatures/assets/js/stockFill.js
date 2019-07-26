"use strict";

function changeLabelPlusOne(button) {
    var $label = $(button).closest("td").find(".stockLevelLabel");
    var $hidden = $(button).closest("td").find(".quantity");

    var value = parseInt($label.text());
    value = isNaN(value) ? 0 : value;
    value++;

    $label.text(value);
    $hidden.val(value);
}

function changeLabelMinusOne(button) {
    var $label = $(button).closest("td").find(".stockLevelLabel");
    var $hidden = $(button).closest("td").find(".quantity");

    var value = parseInt($label.text());
    value = isNaN(value) ? 0 : value;
    if (value == 0) {
        $label.text(value);
        $hidden.val(value);
    } else {
        value = value - 1;
        $label.text(value);
        $hidden.val(value);
    }
}
