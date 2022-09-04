# webtutsplus-backend
.Net Core w/Dapper webAPI version of the webtutsplus java ecommerce-backend

This is a .Net Core drop-in replacement for the java-based ecommerce-backend https://github.com/webtutsplus/ecommerce-backend
using Dapper and MySQL. The VUE frontend is available here: https://github.com/webtutsplus/ecommerce-vuejs

All the basic catalog functionality is working, except the Stripe checkout. The roles need to be audited a little more. Admin is fully functional. It's possible users could do some admin functions. This needs more testing.

To make it a 100% drop-in replacement, meaning no code changes in the VUE frontend, a few controllers are not enforcing token authentication, so it is not suitable for production use. It would be simple to turn on authentication on all controllers, but it would require some small changes in the VUE client, and the goal for this project was 100% compatibility with the Vue.js frontend.

The base of this project, coded using the Repository Pattern with Dapper, is well documented here:

https://code-maze.com/using-dapper-with-asp-net-core-web-api/

The Vue.js frontend is online here:

https://simplecoding-ecommerce.netlify.app/

Note: The framework is added to make the catalog work with multiple stores. It couldn't be fully implemented without some changes to the front-end or getting the project to the point where user authentication is fully implemented. The ability for the Admin code to handle multiple stores is probably around 80% completed, and the remaining changes needed to fully add this feature is minimal.












