# webtutsplus-backend
.Net Core w/Dapper webAPI version of the webtutsplus java ecommerce-backend

This is a .Net Core drop-in replacement for the java-based ecommerce-backend https://github.com/webtutsplus/ecommerce-backend
using Dapper and MySQL as the database. The VUE frontend is available here: https://github.com/webtutsplus/ecommerce-vuejs

All the basic catalog functionality is working, except the Stripe checkout, the roles need to be audited a little more. Admin is fully functional. It's possible users could do some admin functions. This needs more testing.

To make it a 100% drop-in replacement, meaning no code changes in the VUE frontend, a few controllers are not enforcing token authentication, so it is not suitable for production use. It would be simple to turn on authentication on all controllers, but it would require some small changes in the VUE client, and the goal for this project was 100% compatability with the Vue.js frontend.





