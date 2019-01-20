"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BookingSearch = /** @class */ (function () {
    function BookingSearch(id, bookingId, shipNames) {
        if (id === void 0) { id = 0; }
        if (bookingId === void 0) { bookingId = null; }
        if (shipNames === void 0) { shipNames = null; }
        this.id = id;
        this.bookingId = bookingId;
        this.shipNames = shipNames;
    }
    return BookingSearch;
}());
exports.BookingSearch = BookingSearch;
//# sourceMappingURL=bookingsearch.model.js.map