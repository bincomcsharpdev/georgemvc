document.getElementById("taxForm").addEventListener("submit", function (event) {
    const incomeInput = document.getElementById("income");
    const errorMessage = document.getElementById("error-message");
    const incomeValue = incomeInput.value.trim();

    incomeInput.addEventListener("input", function (e) {
        let value = e.target.value.replace(/,/g, ''); // Remove existing commas
        if (!isNaN(value) && value) {
            e.target.value = Number(value).toLocaleString(); // Add commas
        }
    });

    // Clear any previous error messages
    errorMessage.style.display = "none";
    errorMessage.textContent = "";

    // Validate input
    if (incomeValue === "") {
        event.preventDefault(); // Prevent form submission
        errorMessage.textContent = "Please enter a number.";
        errorMessage.style.display = "block";
    } else if (isNaN(incomeValue)) {
        event.preventDefault();
        errorMessage.textContent = "Please enter a valid number.";
        errorMessage.style.display = "block";
    } else if (parseFloat(incomeValue) < 0) {
        event.preventDefault();
        errorMessage.textContent = "Income cannot be negative.";
        errorMessage.style.display = "block";
    }
});
