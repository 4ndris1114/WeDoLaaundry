function checkSlots() {
    var validationTextPU = document.getElementById('validationTextPU');
    var validationTextR = document.getElementById('validationTextR');

    var pickupSlot = document.getElementById('pickupDay');
    var returnSlot = document.getElementById('returnDay');

    var btnSubmit = document.getElementById('btnSubmit');

    validationTextPU.style.display = 'none';
    validationTextR.style.display = 'none';

    btnSubmit.disabled = false;

    //check for null value
    if (pickupSlot.value == "") {
        validationTextPU.style.display = 'block';
        validationTextPU.innerHTML = "You need to select the date & time!";
        btnSubmit.disabled = true;
    }
    if (returnSlot.value == "") {
        validationTextR.style.display = 'block';
        validationTextR.innerHTML = "You need to select the date & time!";
        btnSubmit.disabled = true;
    }

    //check if values are matching, alert user + submit disable button
    if (pickupSlot.value == returnSlot.value && pickupSlot.value != "" && returnSlot.value != "") {
        validationTextPU.style.display = 'block';
        validationTextR.style.display = 'block';
        validationTextPU.innerHTML = "Collection time can not be the same as delivery time!";
        validationTextR.innerHTML = "Delivery time can not be the same as collection time!";
        btnSubmit.disabled = true;

    }
}