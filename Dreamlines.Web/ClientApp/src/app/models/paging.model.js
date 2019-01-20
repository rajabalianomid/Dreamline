"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Paging = /** @class */ (function () {
    function Paging(data, pageIndex, pageSize) {
        if (pageIndex === void 0) { pageIndex = 0; }
        if (pageSize === void 0) { pageSize = 10; }
        this.data = data;
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
    }
    Paging.prototype.preparePageSize = function (newItem, oldItem) {
        oldItem.previous = newItem.previous;
        oldItem.next = newItem.next;
        oldItem.total = newItem.count;
        oldItem.pageIndex = newItem.pageIndex;
        oldItem.data = oldItem.data;
    };
    return Paging;
}());
exports.Paging = Paging;
//# sourceMappingURL=paging.model.js.map