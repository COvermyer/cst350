// js/modalHandler.js

function bindEditProductEvent(taxRate) {
    $(document).on('click', '.edit-product', function (e) {
        e.preventDefault();

        // get the product id from the data-id attribute in the HTML element
        var productId = $(this).data('id');
        $.ajax({
            url: '/Home/ShowUpdateProductForm',
            data: { id: productId },
            success: function (result) {
                // Load the result into the modal content
                $('#editProductModal .modal-content').html(result);

                // make sure the image URL is set to the current product image
                var currentImage = $(`div[data-id="` + productId + '"] img').attr('src');
                currentImage = currentImage.replace("/images/", "").trim(); // remove the /images/ part
                $('#ImageUrl').val(currentImage);

                // use the Bootstrao modal to display the modal
                var modal = new bootstrap.Modal(document.getElementById('editProductModal'));
                modal.show();

                // initialize the dynamically loaded form
                initializeForm(taxRate);
            },
            error: function() {
                console.error('An error occurred while loading the eform.')
            }
        });
    });
}