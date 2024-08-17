document.addEventListener('DOMContentLoaded', function () {
    console.log('DOM loaded');

    const galleryContainer = document.querySelector('.gallery-container');
    const galleryControlsContainer = document.querySelector('.gallery-controls');
    const galleryControls = ['previous', 'next'];
    const galleryItems = document.querySelectorAll('.card');

    if (!galleryContainer || !galleryControlsContainer || galleryItems.length === 0) {
        console.log('Required elements not found. Gallery: ', !!galleryContainer,
            'Controls: ', !!galleryControlsContainer,
            'Items: ', galleryItems.length);
        return;
    }

    class Carousel {
        constructor(container, items, controls) {
            this.carouselContainer = container;
            this.carouselControls = controls;
            this.carouselArray = [...items];
        }

        updateGallery() {
            this.carouselArray.forEach((el, i) => {
                el.classList.remove('gallery-item-1');
                el.classList.remove('gallery-item-2');
                el.classList.add(`gallery-item-${i + 1}`);
            });
        }

        setCurrentState(direction) {
            if (direction.includes('previous')) {
                this.carouselArray.unshift(this.carouselArray.pop());
            } else {
                this.carouselArray.push(this.carouselArray.shift());
            }
            this.updateGallery();
        }

        setControls() {
            console.log('Setting controls');
            this.carouselControls.forEach(control => {
                const button = document.createElement('button');
                button.className = `gallery-controls-${control}`;
                galleryControlsContainer.appendChild(button);
                console.log(`Added ${control} button`);
            });
        }

        useControls() {
            const triggers = [...galleryControlsContainer.childNodes];
            triggers.forEach(control => {
                control.addEventListener('click', e => {
                    e.preventDefault();
                    this.setCurrentState(control.className);
                });
            });
        }
    }

    const carousel = new Carousel(galleryContainer, galleryItems, galleryControls);
    carousel.setControls();
    carousel.useControls();
});
