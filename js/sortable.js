// *** sortable.js - https://github.com/rern/js/edit/master/sortable/ ***
/*
usage:
...
<link rel="stylesheet" href="/path/sortable.css">
</head>
<body>
	<div id="divbeforeid"> <!-- optional -->
		(divBeforeTable html)
	</div>
	<table id="tableid">
		<thead><tr><td></td></tr></thead>
		<tbody><tr><td></td></tr></tbody>
	</table>
	<div id="divafterid"> <!-- optional -->
		(divAfterTable html)
	</div>
<script src="/path/jquery.min.js"></script>
<script src="/path/sortable.js"></script>
<script>
...
$('tableid').sortable();             // without options > full page table
// or
$('tableid').sortable( {
	  divBeforeTable:  'divbeforeid' // default: (none) - div before table, enclosed in single div
	, divAfterTable:   'divafterid'  // default: (none) - div after table, enclosed in single div
	, initialSort:     'column#'     // default: (none) - start with 0
	, initialSortDesc: true          // default: false
	, locale:          'code'        // default: 'en'   - locale code
	, negativeSort:    [column#]     // default: (none) - column with negative value
	, tableArray:      []            // default: (none) - use table data array directly
} );
...

custom css for table:
edit in sortable.css  
*/

( function ( $ ) {

$.fn.sortable = function ( options ) {
//*****************************************************************************
var settings = $.extend( { // defaults
	divBeforeTable: ''
	, divAfterTable: ''
	, initialSort: ''
	, initialSortDesc: false
	, locale: 'en'
	, negativeSort: []
	, tableArray : []
}, options );
var shortViewportH = 414; // max height to apply fixed 'thead2'
var timeout = 400; // try higher to fix incorrect alignment

var $window = $( window );
var $table = this;
var $thead = $table.find( 'thead' );
var $thtr = $thead.find( 'tr' );
var $thtd = $thtr.children(); // either 'th' or 'td'
var $tbody = $table.find( 'tbody' );
var $tbtr = $tbody.find( 'tr' );
var $tbtd = $tbtr.find( 'td' );

// use table array directly if provided
if ( settings.tableArray.length ) {
	var tableArray = settings.tableArray;
} else {
	// convert 'tbody' to value-only array [ [i, 'a', 'b', 'c', ...], [i, 'd', 'e', 'f', ...] ]
	var tableArray = [];
	$tbtr.each( function ( i ) {
		var row = [ i ];
		$( this ).find( 'td' ).each( function ( j ) {
			if ( settings.negativeSort.indexOf( j+1 ) === -1 ) { // '+1' - make 1st column = 1, not 0
				var cell = $( this ).text();
			} else { // get minus value in alphanumeric column
				var cell = $( this ).text().replace( /[^0-9\.\-]/g, '' ); // get only '0-9', '.' and '-'
			}
			row.push( $thtd.eq( j ).text() == '' ? '' : cell ); // blank header not sortable
		} );
		tableArray.push( row );
	} );
}

if ( settings.divBeforeTable ) {
	$( settings.divBeforeTable ).addClass( 'divbefore' );
	var divBeforeH = $( settings.divBeforeTable ).outerHeight();
} else {
	var divBeforeH = 0;
}
if ( settings.divAfterTable ) {
	$( settings.divAfterTable ).addClass( 'divafter' );
	var divAfterH = $( settings.divAfterTable ).outerHeight();
} else {
	var divAfterH = 0;
}

// dynamic css - for divBeforeH underlay, divAfterH and fixed thead2
var tableID = this[ 0 ].id;
var tableParent = '#sortable'+ tableID;
$table.wrap( '<div id="sortable'+ tableID +'" class="tableParent"></div>' );
$table.addClass( 'sortable' );
var trH = $tbtr.height();

$( 'head' ).append( '<style>'
	+'.tableParent::before {'
		+'content: "";'
		+'display: block;'
		+'height: '+ divBeforeH +'px;'
		+'width: 100%;'
	+'}\n'
	+'.sortableth2 {top: '+ divBeforeH +'px;}\n'
	+'#trlast {height: '+ ( divAfterH + trH ) +'px;}\n'
	+'@media(max-height: '+ shortViewportH +'px) {\n'
		+'.divbefore {position: absolute;}'
		+'.divafter {position: relative;}'
		+'.sortableth2 {top: 0;}'
		+'.sortable thead {visibility: visible;}'
		+'#trlast {height: '+ trH +'px;}'
	+'}'
	+'</style>'
//	+'<meta name="viewport" content="width=device-width, initial-scale=1.0">'
);

// #1 - functions
// get scroll position
var positionTop = 0;
var scrollTimeout;
function getScrollTop() {
	$window.scroll( function () {
		// cancel previous 'scroll' within 'timeout'
		clearTimeout( scrollTimeout );
		scrollTimeout = setTimeout( function () {
			positionTop = $window.scrollTop();
		}, timeout );
	} );
};

// #2 - add l/r padding 'td' to keep table center
var $tabletmp = $table.detach(); // avoid many dom traversings (but cannot maintain width)
$thtd.addClass( 'asctmp' ); // add sort icon to allocate width
// change 'th' to 'td'
$thtd.prop( 'tagName' ) == 'TH' && $thtr.html( $thtr.html().replace( /th/g, 'td' ) );
// add 'tdpad'
$thtr.add( $tbtr )
	.prepend( '<td class="tdpad"></td>' )
	.append( '<td class="tdpad"></td>' )
;
// refresh cache after add 'tdpad'
$thtd = $thtr.find( 'td' );
$tbtd = $tbtr.find( 'td' );

// #3 - add fixed header for short viewport
var thead2html = '<a></a>';
$( tableParent ).append( $tabletmp )
	.find( 'thead td' ).each( function ( i ) { // allocate width for sort icon
		if ( i > 0 && i < ( $thtd.length - 1 ) ) {
			var tdW = $( this ).outerWidth();
			$( this ).css( 'min-width', tdW  +'px' );
			var tdHide = $( this ).is( ':hidden' ) ? 'display: none;' : '';
			thead2html += '<a style="' // prepare 'thead2'
				+'width: '+ tdW  +'px;'
				+'text-align: '+ $( this ).css( 'text-align' ) +';'
				+ tdHide
				+'">'+ $( this ).text() +'</a>'
			;
		}
	} ).removeClass( 'asctmp' )
;

$( 'body' ).prepend(
	'<div id="'+ tableID +'th2" class="sortableth2" style="display: none;">'+ thead2html +'</div>'
);
var $thead2 = $( '#'+ tableID +'th2' );
var $thead2a = $thead2.find( 'a' );
setTimeout( function () { // delay for 'tdpad' width
	$thead2a.eq( 0 )
		.css('width', $thtd.eq( 0 ).outerWidth() +'px' )
			.parent()
				.show();
}, 300 );
// delegate click to 'thead'
$thead2a.click( function () {
	$thtd.eq( $( this ).index() ).click();
} );

// #4 - click 'thead' to sort
$thtd.click( function ( event, initdesc ) {
	var i = $( this ).index();
	var order = ( $( this ).hasClass( 'asc' ) || initdesc ) ? 'desc' : 'asc';
	// sort value-only array (multi-dimensional)
	var sorted = tableArray.sort( function ( a, b ) {
		var ab = ( order == 'desc' ) ? [ a, b ] : [ b, a ];
		if ( settings.negativeSort.indexOf( i ) === -1 ) {
			return ab[ 0 ][ i ].localeCompare( ab[ 1 ][ i ], settings.locale, { numeric: true } );
		} else {
			return ab[ 0 ][ i ] - ab[ 1 ][ i ];
		}
	} );
	// sort 'tbody' in-place by each 'array[ 0 ]', reference i [ [i, 'a', 'b', 'c', ...], [i, 'd', 'e', 'f', ...] ]
	var $tbodytmp = $tbody.detach();
	$thead2a.add( $thtd ).add( $tbtd )
		.removeClass( 'asc desc sorted' );
	$.each( sorted, function () {
		$tbodytmp.prepend( $tbtr.eq( $( this )[ 0 ] ) );
	} );
	// switch sort icon and highlight sorted column
	$thead2a.eq( i ).add( this )
		.addClass( order )
			.add( $tbody.find( 'td:nth-child('+ ( i+1 ) +')' ) )
				.addClass( 'sorted' )
	;
	$table.append( $tbodytmp );
} );

// #5 - add empty 'tr' to bottom then initial sort
$tbody.append(
	$tbody.find( 'tr:last' )
		.clone()
		.empty()
		.prop( 'id', 'trlast' )
).parent() // initial sort
	.find( $thtd ).eq( settings.initialSort )
		.trigger( 'click', settings.initialSortDesc )
;

// #6 - maintain scroll position on rotate
getScrollTop();
// reference for scrolling calculation
var fromShortViewport = ( $window.height() <= shortViewportH ) ? 1 : 0;
var positionCurrent = 0;
window.addEventListener( 'orientationchange', function () {
	$window.off( 'scroll' ); // suppress new 'scroll'
	$thead2.hide(); // avoid 'thead2' unaligned flash
	// maintain scroll (get 'scrollTop()' here works only on ios)
	if ( $( '.sortableth2' ).css( 'top' ) == '0px' ) {
		positionCurrent = positionTop + divBeforeH;
		fromShortViewport = 1;
	} else {
		// omit 'divBeforeTable' if H to V from short viewport
		positionCurrent = positionTop - ( fromShortViewport ? divBeforeH : 0 );
		fromShortViewport = 0;
	}
	positionTop = positionCurrent; // update to new value
	setTimeout( function () {
		$window.scrollTop( positionCurrent );
		getScrollTop();
	}, timeout );
} );

// #7 - realign 'thead2' on rotate / resize
var resizeTimeout;
window.addEventListener( 'resize', function () {
	// cancel previous 'resize' within 'timeout'
	clearTimeout( resizeTimeout );
	resizeTimeout = setTimeout( function () {
		$thead2a.show(); // reset hidden
		$thtd.each( function ( i ) {
			$thead2a.eq( i ).css( 'width', $( this ).outerWidth() +'px' ); // for changed width
			$( this ).is( ':hidden' ) && $thead2a.eq( i ).hide();
		} );
		$thead2.show();
	}, timeout );
} );
//*****************************************************************************
}
} ( jQuery ) );
