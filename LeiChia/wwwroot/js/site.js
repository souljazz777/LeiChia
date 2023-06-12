function TurnToHome() {
    window.location.href = '/Home/Index';
}


function decrementStock() {
    var input = $(".quantity");
    var value = parseInt(input.val());
    
    if (value == 0) {
        input.val(0);
    }
    else {
        value = value - 1;
    }
    input.val(value);
}
function incrementStock() {
    var input = $(".quantity");
    var value = parseInt(input.val());
    value = value + 1;
    input.val(value);
}
function AddToCart(id) {
    var input = $(".quantity");
    var quantity = input.val();
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



function Edit(id) {
    var input = $(".quantity");
    var quantity = input.val();
    $.ajax({
    type: 'post',
        url: '/Product/EditItem',
        data: {
            id: parseInt(id),
            quantity: parseInt(quantity)
        },
        success: function (response) {
            alert('更新成功')
            window.location.reload()
        },
        error: function (response) {
            console.log(response)
        }
    });
}



