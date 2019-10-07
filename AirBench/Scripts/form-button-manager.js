(() =>{

    function manageForm(form){
        const button = form.querySelector('input[type="submit"]');
        const requiredInputs = Array.from(form.querySelectorAll('input[data-val-required]'));
        console.log(requiredInputs);
        
        button.disabled=true;

        form.addEventListener('keyup', () => {
            button.disabled = requiredInputs.some(ri => ri.value==='');
        });


    }
        
    //TODO get reference to form
    const formElements = document.querySelectorAll('.manage-submit-button');
    formElements.forEach(form => manageForm(form));

})();