import React from 'react';

import PageItem from './page-item';

class CommentPage extends React.Component {

    constructor() {
        super();
    }

    render() {
        var count = this.props.pageCount;
        var pageNum = this.props.CurrentPage;
        var items =[];
        for (var i=1;i<=count;i++){
            items.push(<PageItem key={i} value={i} changePage={this.props.changePage} />);
        }

        return (
            <ul className="d-flex none-style-ul">
                {items}
            </ul>
        );
    }
}

export default CommentPage;
