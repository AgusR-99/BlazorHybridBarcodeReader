/*!
* Start Bootstrap - New Age v6.0.7 (https://startbootstrap.com/theme/new-age)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-new-age/blob/master/LICENSE)
*/
//
// Scripts
//

window.addEventListener('DOMContentLoaded', event => {
    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            offset: 74,
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });
});

//Pop up para poder ver un pdf en otra pestaña. Agustin 17/10/2023
window.jsInteropFunctions = {
    openPdfInNewTab: function (pdfBase64) {
        // Convierte la cadena base64 en un ArrayBuffer
        const binaryString = atob(pdfBase64);
        const length = binaryString.length;
        const bytes = new Uint8Array(length);

        for (let i = 0; i < length; i++) {
            bytes[i] = binaryString.charCodeAt(i);
        }

        // Crea un Blob y una URL del objeto Blob
        const blob = new Blob([bytes], { type: "application/pdf" });
        const blobUrl = URL.createObjectURL(blob);

        // Abre una nueva pestaña con el PDF
        window.open(blobUrl);
    }
};

let activityDetected;
let timeoutId;

window.detectUserActivity = (dotnetHelper) => {
    if (activityDetected) {
        console.log("Event listener already exists");
        return;
    }

    activityDetected = () => {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(() => {
            console.log("User event detected");
            dotnetHelper.invokeMethodAsync('UserActivityDetected');
        }, 500);
    };

    console.log("Event listener added");
    document.addEventListener('mousemove', activityDetected);
    document.addEventListener('keydown', activityDetected);
};

window.removeUserActivityDetection = () => {
    if (!activityDetected) {
        console.log("No event listener to remove");
        return;
    }

    console.log("Event listener removed");
    document.removeEventListener('mousemove', activityDetected);
    document.removeEventListener('keydown', activityDetected);
    clearTimeout(timeoutId);
    activityDetected = null;
};

function removeButtons() {
    const buttonsToRemove = document.querySelectorAll('.modal-body button[data-action]');
    buttonsToRemove.forEach(button => button.remove());
}

function modifySvgViewbox(id) {
    const concatenated = '#' + id + ' svg';
    const svg = document.querySelector(concatenated);
    if (svg) {
        // Get the 4rd element of the viewBox attribute
        const viewBox = svg.getAttribute('viewBox').split(' ');
        viewBox[3] = '50';
        svg.setAttribute('viewBox', viewBox.join(' '));

    } else {
        console.error('SVG element not found');
    }
}