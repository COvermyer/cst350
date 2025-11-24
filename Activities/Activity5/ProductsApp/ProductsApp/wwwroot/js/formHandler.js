// js/formHandler.js

function bindFormSubmission() {
    // when a form is submitted, the following event will be called
    $(document).on('submit', 'form', function (e) {
        e.preventDefault();
        var form = $(this);
        $.ajax({
            url: '/Home/UpdateProductFromModal',
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                // response is the HTML content from the updated product card
                var productId = form.find('input[name="Id"]').val();

                if ($(response).find('form').length > 0) {
                    // if the response contains a form, validation has failed. Pass the form back to the modal
                    $('#editProductModal .modal-content').html(response)
                } else { // validation has passed
                    // replace the existing card with the updated content
                    $('div[data-id="' + productId + '"]').html(response);

                    // hide the modal
                    var modal = bootstrap.Modal.getInstance(document.getElementById('editProductModal'));
                    modal.hide();
                }
            },
            error: function () {
                console.error('An error occurred while submitting the form.');
            }
        });
    });
}