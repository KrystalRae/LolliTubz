"use strict";

var pageTotal = 0;

$(document).ready(function () {
    calculateRowTotal();
});
//neddto make method so row totals are calculated when you first enter the edit order screen
function changeLabelPlusOne(button) {
    var $label = $(button).closest("td").find(".stockLevelLabel");
    var $hidden = $(button).closest("td").find(".quantity");

    var value = parseInt($label.text());
    value = isNaN(value) ? 0 : value;
    value++;

    $label.text(value);
    $hidden.val(value);
    var cost = $(button).closest("tr").find(".cost");
    var price = parseFloat(cost.text());
    var rowTotal = (value * price).toFixed(2)
    $(button).closest("tr").find(".totalRowValue").text(`$${rowTotal}`);

    pageTotal += price;
    $("#pageTotal").text(`$${parseFloat(pageTotal).toFixed(2)}`);
}


function calculateRowTotal() {
    let row = $('.stockLevelLabel');

    for (let i = 0; i < row.length; i++) {
        var $label = $(row[i]);
        var value = parseInt($label.text());
        var cost = $(row[i]).closest("tr").find(".cost");
        var price = parseFloat(cost.text());
        var rowTotal = (value * price).toFixed(2)
        $(row[i]).closest("tr").find(".totalRowValue").text(`$${rowTotal}`);

        pageTotal += parseFloat(rowTotal);
    }
        
    $("#pageTotal").text(`$${parseFloat(pageTotal).toFixed(2)}`);
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
        var cost = $(button).closest("tr").find(".cost");
        var price = parseFloat(cost.text());
        var rowTotal = (value * price).toFixed(2)
        $(button).closest("tr").find(".totalRowValue").text(`$${rowTotal}`);

        pageTotal -= price;
        $("#pageTotal").text(`$${parseFloat(pageTotal).toFixed(2)}`);
    }
}



