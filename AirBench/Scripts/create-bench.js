const form = document.getElementById('createBenchForm');
const descriptionInput = document.getElementById('Description');
const numberOfSeatsInput = document.getElementById('NumberOfSeats');
const latitudeInput = document.getElementById('Latitude');
const longitudeInput = document.getElementById('Longitude');

const createBenchButton = document.getElementById('Button');

form.addEventListener('keyup', () => {
    createBenchButton.disabled = (
        (descriptionInput.value==='')||
        (numberOfSeatsInput.value==='')||
        (latitudeInput.value==='')||
        (longitudeInput.value==='')
    );
});