# _Band Tracker_

#### _A web app to track bands and the venues where they've performed, 08.25.17_

#### By _**Kaili Nishihira**_

## Description

_A web app which will enable the user to enter venues and bands. The user may view all of the venues entered and the bands which have played at a selected venue. The user may add a band to the venue that has played at the selected venue. The user may view all of the bands and the venues which have hosted the selected band. The user may add a venue to a band that has hosted the selected band. Users may also update band names and deleted selected bands._

|| Behavior  | Input  | Output  |
|---|---|---|---|
|| User may view a list of all bands   | Click `All bands`  | All Bands: <li>Rilo Kiley</li> <li>The Beatles</li>  |
|| User may enter a new band <li>Form to enter the band's information in 'Bands' view</li> | Enter a new band: <br> Band Name: 'The Beatles' <br> Click `Add`| All Bands: <br> ... <br> The Beatles <br> ... |
|| User may view a band's details  | Click 'The Beatle'  | The Beatles <br> Venues Played: <br>  |
|| User may view a list of all venues  | Click `All venues`  | All Venues: <li>Red Rocks Amphitheatre</li> <li>Waikiki Shell</li>  |
|| User may enter a new venue <li>Form to enter the venue's information in "Venues" view</li> | Venue Name: 'Red Rocks Amphitheatre' <br> City: 'Morrison' <br> Click `Add`| All Venues: <br> ... <br> Red Rocks Amphitheatre <br> ... |
|| User may add a new venue to a specific band <br> Drop down menu to add previously entered bands <br> Form will be in 'Band Details' view to add new Venue  | Enter a new venue for The Beatles: <br> Venue Name: 'Waikiki Shell' <br> City: 'Honolulu, HI' | The Beatles <br> Venues Played: <br> ... <br> Waikiki Shell <br> ... |
|| User may view a venues's details  | Click on 'Waikiki Shell'  | Waikiki Shell <br> Honolulu <br> Hosted Bands: <br> ... <br> The Beatles <br> ...   |
|| User may update a venue's information <li>Click on venue's name</li> <li>View returns the venue's details</li> <li>Click `edit`</li><li>View returns a form to update the venue's information</li>  | Update details for waikiki shell: <br> Venue Name: 'Waikiki Shell' <br> Click `update` </li> | Waikiki Shell |
|| User may update a band's name <li>Click on band's name</li> <li>View returns the band's details</li> <li>Click `edit`</li><li>View returns a form to update the band's name</li>  | Update details for The beatles: <br> Band Name: 'The Beatles <br> Click `update` </li> | The Beatles |
|| User may delete a venue <li>Click on venue's name</li> <li>View returns the venue's details</li>  | Click `delete`  | View returns to 'Venues' and deleted venue is no longer in the list  |
|| User may delete a band <li>Click on band's name</li> <li>View returns the band's details</li>  | Click `delete`  | View returns to 'Bands' and deleted band is no longer in the list  |
|| User may delete a band from a venue <li>Click on venue's name</li> <li>View returns the venue's details with a list of bands the venue has hosted</li> | Click `x` next to the band's name in the 'Bands Hosted' list| View returns the venue's details without the selected band in the 'Bands Hosted' list |
|| User may delete a venue from a band <li>Click on band's name</li> <li>View returns the band's details with a list of venues played</li> | Click `x` next to the venue's name in the 'Venues Played' list | View returns the band's details without the selected venue in the 'Venues Played' list |




## Setup/Installation Requirements

* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Set MySQL Port to 3306_
* _Clone repository_

#### There are two options to create the database:
##### 1. In MySQL
`> CREATE DATABASE band_tracker;`<br>
`> USE band_tracker;`<br>
`> CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255));`<br>
`> CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255), city(255));`<br>
`> CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT, venue_id INT);`<br>

##### 2. Import from the Cloned Repository
* _Click 'Open start page' in MAMP_
* _Under 'Tools', select 'phpMyAdmin'_
* _Click 'Import' tab_
* _Choose database file (from cloned repository folder)_
* _Click 'Go'_

## SQL Design
![](/sql-design.png)

## Technologies Used
* _C#_
* _.NET_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[MySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **_Kaili Nishihira_**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*
