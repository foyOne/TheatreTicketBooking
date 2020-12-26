// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


let reservationSeats = {
    "Level1": new Array(),
    "Level2": new Array(),
    "Level3": new Array()
};

function changeReservation() {

    let status = this.parentNode.parentNode.firstElementChild.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
    if (status != "Свободно")
        alert("Это место уже забронировано");
    else {
        let level = this.parentNode.parentNode.firstElementChild.firstElementChild.innerHTML;
        let seatNumber = this.parentNode.parentNode.firstElementChild.nextElementSibling.firstElementChild.innerHTML;
        level = "Level" + level;
        if (this.innerHTML == "+") {
            reservationSeats[level].push(seatNumber);
            this.innerHTML = "-";
        }
        else {
            let elementIndex = reservationSeats[level].indexOf(seatNumber);
            if (elementIndex != -1)
                reservationSeats[level].splice(elementIndex, 1);
            this.innerHTML = "+";
        }
    }
    //for (key in reservationSeats)
    //    console.log(reservationSeats[key]);
}

function reservation() {
    document.reservation["action"] += "?";
    let Level1 = "Level1=" + reservationSeats["Level1"].join(",");
    let Level2 = "Level2=" + reservationSeats["Level2"].join(",");
    let Level3 = "Level3=" + reservationSeats["Level3"].join(",");


    document.reservation["action"] += Level1 + "&";
    document.reservation["action"] += Level2 + "&";
    document.reservation["action"] += Level3 + "&";

    for (key in reservationSeats)
        reservationSeats[key].length = 0;
    //console.log(document.reservation["action"]);
}

document.getElementById("submitForm").addEventListener("click", reservation);

Array.from(document.getElementsByName("changeSeat")).forEach(f => {
    f.addEventListener("click", changeReservation);
});
