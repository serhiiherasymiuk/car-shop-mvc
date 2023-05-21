function decodeVin(vin) {
    return fetch(`https://vpic.nhtsa.dot.gov/api/vehicles/decodevin/${vin}?format=json`)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const make = data.Results.find(r => r.Variable === "Make")?.Value;
            const model = data.Results.find(r => r.Variable === "Model")?.Value;
            const year = data.Results.find(r => r.Variable === "Model Year")?.Value;
            const plantCountry = data.Results.find(r => r.Variable === "Plant Country")?.Value;
            const plantState = data.Results.find(r => r.Variable === "Plant State")?.Value;
            const series = data.Results.find(r => r.Variable === "Series")?.Value;
            const vehicleType = data.Results.find(r => r.Variable === "Vehicle Type")?.Value;
            const plantCompanyName = data.Results.find(r => r.Variable === "Plant Company Name")?.Value;
            const basePrice = data.Results.find(r => r.Variable === "Base Price ($)")?.Value;
            const bodyClass = data.Results.find(r => r.Variable === "Body Class")?.Value;
            const doors = data.Results.find(r => r.Variable === "Doors")?.Value;
            const seats = data.Results.find(r => r.Variable === "Number of Seats")?.Value;
            const engineCylinders = data.Results.find(r => r.Variable === "Engine Number of Cylinders")?.Value;
            const enginePower = data.Results.find(r => r.Variable === "Engine Power (kW)")?.Value;
            const fuelType = data.Results.find(r => r.Variable === "Fuel Type - Primary")?.Value;
            const topSpeed = data.Results.find(r => r.Variable === "Top Speed (MPH)")?.Value;
            const engineManufacturer = data.Results.find(r => r.Variable === "Engine Manufacturer")?.Value;
            const seatBeltType = data.Results.find(r => r.Variable === "Seat Belt Type")?.Value;

            return {
                make,
                model,
                year,
                plantCountry,
                plantState,
                series,
                vehicleType,
                plantCompanyName,
                basePrice,
                bodyClass,
                doors,
                seats,
                engineCylinders,
                enginePower,
                fuelType,
                topSpeed,
                engineManufacturer,
                seatBeltType
            };
        })
        .catch(error => console.error(error));
}

$(document).ready(() => {
    $("#decode-btn").click(() => {
        const vin = $("#vin").val();
        decodeVin(vin)
            .then(result => {
                const make = result.make || "N/A";
                const model = result.model || "N/A";
                const year = result.year || "N/A";
                const plantCountry = result.plantCountry || "N/A";
                const plantState = result.plantState || "N/A";
                const series = result.series || "N/A";
                const vehicleType = result.vehicleType || "N/A";
                const plantCompanyName = result.plantCompanyName || "N/A";
                const basePrice = result.basePrice || "N/A";
                const bodyClass = result.bodyClass || "N/A";
                const doors = result.doors || "N/A";
                const seats = result.seats || "N/A";
                const engineCylinders = result.engineCylinders || "N/A";
                const enginePower = result.enginePower || "N/A";
                const fuelType = result.fuelType || "N/A";
                const topSpeed = result.topSpeed || "N/A";
                const engineManufacturer = result.engineManufacturer || "N/A";
                const seatBeltType = result.seatBeltType || "N/A";

                $("#result").html(`
                    <p><strong>Make:</strong> ${make}</p>
                    <p><strong>Model:</strong> ${model}</p>
                    <p><strong>Year:</strong> ${year}</p>
                    <p><strong>Plant Country:</strong> ${plantCountry}</p>
                    <p><strong>Plant State:</strong> ${plantState}</p>
                    <p><strong>Series:</strong> ${series}</p>
                    <p><strong>Vehicle Type:</strong> ${vehicleType}</p>
                    <p><strong>Plant Company Name:</strong> ${plantCompanyName}</p>
                    <p><strong>Base Price ($):</strong> ${basePrice}</p>
                    <p><strong>Body Class:</strong> ${bodyClass}</p>
                    <p><strong>Doors:</strong> ${doors}</p>
                    <p><strong>Number of Seats:</strong> ${seats}</p>
                    <p><strong>Engine Number of Cylinders:</strong> ${engineCylinders}</p>
                    <p><strong>Engine Power (kW):</strong> ${enginePower}</p>
                    <p><strong>Fuel Type - Primary:</strong> ${fuelType}</p>
                    <p><strong>Top Speed (MPH):</strong> ${topSpeed}</p>
                    <p><strong>Engine Manufacturer:</strong> ${engineManufacturer}</p>
                    <p><strong>Seat Belt Type:</strong> ${seatBeltType}</p>
                `);
            })
            .catch(error => console.error(error));
    });
});