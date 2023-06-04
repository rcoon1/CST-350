// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    console.log("document is ready");
    $(document).on("click", ".edit-product-button", function () {
        console.log("You just clicked button number " + $(this).val());

        var productID = $(this).val();

        $.ajax({
            type: 'json',
            data: {
                "id": productID
            },
            url: '/products/ShowOneProductJSON',
            success: function (data) {
                console.log(data)

                // fill the input fields in the modal

                $("#modal-input-id").val(data.id);
                $("#modal-input-name").val(data.name);
                $("#modal-input-price").val(data.price);
                $("#modal-input-description").val(data.description);


            }
        });

    });

    $("#save-button").click(function () {
        // get the values of the input fields and make a product JSON object.
        var Product = {
            "Id": $("#modal-input-id").val(),
            "Name": $("#modal-input-name").val(),
            "Price": $("#modal-input-price").val(),
            "Description": $("#modal-input-description").val(),
        }
        console.log("saved...");
        console.log(Product);


        // save the updated product record in the database using the controller
        $.ajax({
            type: 'json',
            data: Product,
            url: '/products/ProcessEditReturnPartial',
            success: function (data) {
                //show the partial update fo rtesting purposes.
                console.log(data);
                //replace the proper card with the new data.
                $("#card-number-" + Product.Id).html(data).hide().fadeIn(2000);
            }
        });
    });

});