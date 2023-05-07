function TurnToHome() {
    window.location.href = '/Home/Index';
}


function decrementStock(id) {
    var input = document.getElementById("input-" + id);
    var value = parseInt(input.value) - 1;
    if (value < 0) {
        value = 0;
    }
    input.value = value;
}
function incrementStock(id, max) {
    var input = document.getElementById("input-" + id);
    var value = parseInt(input.value) + 1;
    if (value > max) {
        value = max;
    }
    input.value = value;
}
function AddToCart(id) {
    var input = document.getElementById("input-" + id);
    var quantity = input.value;
    //$('.AddCart').attr('href', '/Product/AddToCart/' + id + '?quantity=' + quantity);

    $.ajax({
        type: 'post',
        url: '/Product/AddToCart',
        data: {
            id: id,
            quantity: quantity
        },
        success: function (response) {
            var modeID = '#modifyStockModal' + id;
            // clean the quantity
            //$(".quantity").val(0);
            // hide model
            $(modeID).modal('hide');
            window.location.href = '/Product/Cart';
        },
        error: function (response) {

        }
    });
}





