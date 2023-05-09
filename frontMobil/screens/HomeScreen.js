 import { useNavigation, useNavigationState } from "@react-navigation/native";
 import React, { useLayoutEffect, useState, useEffect } from "react";
 import { Image, LogBox, ScrollView, Text, TextInput, View } from "react-native";
 import { SafeAreaView } from "react-native-safe-area-context";

 import {
   UserIcon,
   ChevronDownIcon,
   MagnifyingGlassIcon,
   AdjustmentsVerticalIcon,
 } from "react-native-heroicons/outline";
 import Categories from "../components/Categories";
 import FeaturedRow from "../components/FeaturedRow";
 import SanityClient from "../sanity";

 const HomeScreen = () => {
   // state and hooks
   const navigation = useNavigation();
   //const [featuredCategories, setFeaturedCategories] = useState([]);
     const featuredCategories=[
       {
         _id:1,
         name:'Soltana Food',
         image:'https://scontent.ftun14-1.fna.fbcdn.net/v/t1.6435-9/105429256_117045336715598_3692495113764699504_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=e3f864&_nc_ohc=Y1Rx01PCITQAX8Q7iLJ&_nc_ht=scontent.ftun14-1.fna&oh=00_AfDg8CDZmxIQD7diK4nGleUY2Nu_rC1_K3eI6sXI8zig6w&oe=6471E5D9',
         short_description:'tunisian restaurant based in sfaxpreparation of popular Tunisian dishes',
         genre:'tunisien',
         address:'Sfax',
         dishes:[{id:1, name:'kosksi osben', short_description:'plat principale', price:'12', image:'https://pbs.twimg.com/media/FD5lJc4XwAACOWH.jpg:large'}],
         long:'',
         lat:'',
         rating:'4.8'
       }]
   // side effects
   useLayoutEffect(() => {
     navigation.setOptions({
       headerShown: false,
     });
   }, []);
 /*
   useEffect(() => {
     SanityClient.fetch(
       `
       *[_type == "featured"]{
         ...,
         restaurants[]->{
           ...,
         dishes[]->
          
         }
      }
     `,
     )
       .then((data) => setFeaturedCategories(data))
       .catch((err) => {
         console.log("error");
         console.log(err);
       });
   }, []);
 */
   return (
     <>
       <SafeAreaView className="bg-white pt-5">
         {/* header */}
         <View className="flex-row pb-3 items-center mx-4 space-x-2">
         <UserIcon size={25} color="#00CCBB" onPress={() => navigation.navigate('Login')}/>
         <Text className="font-bold text-xl  ">
         </Text>
         <Text className="font-bold text-xl  ">
           Food Delivery
         </Text>
         </View>

         {/* search bar */}
         <View className="flex-row mx-4 items-center space-x-2 pb-2">
           <View className="flex-row space-x-2 flex-1 bg-gray-200 p-3 rounded-md">
             <MagnifyingGlassIcon color="gray" />
             <TextInput placeholder="Restaurants and cuisines" keyboardType="default" />
           </View>
           <AdjustmentsVerticalIcon color="#00CCBB" />
         </View>

         {/* body */}
         <ScrollView className="bg-gray-100" contentContainerStyle={{ paddingBottom: 100 }}>
           {/* categories */}

           {/* featured rows */}
           {featuredCategories.map((category) => (
             <FeaturedRow
               key={category._id}
               title={category.name}
               //description={category.short_description}
               id={category._id}
             />
           ))}
         </ScrollView>
       </SafeAreaView>
     </>
   );
 };

export default HomeScreen;
// import { View, Text, ScrollView, Image, TouchableOpacity } from "react-native";
// import React, { useEffect, useState,useLayoutEffect } from "react";
// import { urlFor } from "../sanity";
// import {
//   ArrowLeftIcon,
//   ChevronRightIcon,
//   MapPinIcon,
//   QuestionMarkCircleIcon,
//   StarIcon,
// } from "react-native-heroicons/solid";
// import DishRow from "../components/DishRow";
// import BasketContainer from "../components/BasketContainer";
// import { useDispatch } from "react-redux";
// import { setRestaurant } from "../slices/restaurantSlice";

// const HomeScreen = ({ route, navigation }) => {
//      const featuredCategories=[
//        {
//          _id:1,
//          name:'Soltana Food',
//          image:'https://scontent.ftun14-1.fna.fbcdn.net/v/t1.6435-9/105429256_117045336715598_3692495113764699504_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=e3f864&_nc_ohc=Y1Rx01PCITQAX8Q7iLJ&_nc_ht=scontent.ftun14-1.fna&oh=00_AfDg8CDZmxIQD7diK4nGleUY2Nu_rC1_K3eI6sXI8zig6w&oe=6471E5D9',
//          short_description:'tunisian restaurant based in sfaxpreparation of popular Tunisian dishes',
//          genre:'tunisien',
//          address:'Sfax',
//          dishes:[{id:1, name:'kosksi osben', description:'', price:'12', image:'https://pbs.twimg.com/media/FD5lJc4XwAACOWH.jpg:large'}],
//          long:'',
//          lat:'',
//          rating:'4.8'
//        }]
//        const [id,setId]=useState(featuredCategories._id) 
//        const [imgUrl,setImgurl]=useState(featuredCategories.image)
//        const [title,setTitle]=useState(featuredCategories.title)
//        const [rating,setRating]=useState(featuredCategories.rating)
//        const [genre,setGenre]=useState(featuredCategories.genre)
//        const [address,setAdress]=useState(featuredCategories.address)
//        const [short_description,setShort_description]=useState(featuredCategories.short_description)
//        const [dishes,setDishes]=useState([featuredCategories.dishes])
//        const [long,setLong]=useState(featuredCategories.long)
//        const [lat,setLat]=useState(featuredCategories.lat)
//   /*const {
//     params: { id, imgUrl, title, rating, genre, address, short_description, dishes, long, lat },
//   } = route;*/
//   useLayoutEffect(() => {
//     navigation.setOptions({
//       headerShown: false,
//     });
//   }, []);
// /*
//   const dispatch = useDispatch();
//   useEffect(() => {
//     dispatch(
//       setRestaurant({
//         id,
//         imgUrl,
//         title,
//         rating,
//         genre,
//         address,
//         short_description,
//         dishes,
//         long,
//         lat,
//       }),
//     );
//   }, [dispatch]);*/

//   return (
//     <>
//       <BasketContainer />
//       <ScrollView>
//         <View className="relative">
//           <Image source={{ uri: imgUrl}} className="w-full h-56 bg-gray-300 p-4" />
//           <TouchableOpacity
//             className="absolute top-14 left-5 p-2 bg-white rounded-full"
//             onPress={() => navigation.goBack(null)}
//           >
//             <ArrowLeftIcon size={20} color="#00ccbb" />
//           </TouchableOpacity>
//         </View>
//         <View className="bg-white">
//           <View className="px-4 pt-4">
//             <Text className="text-3xl font-bold ">{title}</Text>
//             <View className="flex-row space-x-2 my-1">
//               <View className="flex-row items-center space-x-1">
//                 <StarIcon color="green" opacity={0.5} size={22} />
//                 <Text className="text-xs text-gray-500">
//                   <Text className="text-green-500">
//                     {rating} . {genre}
//                   </Text>
//                 </Text>
//               </View>
//               <View className="flex-row items-center space-x-1">
//                 <MapPinIcon color="gray" opacity={0.5} size={22} />
//                 <Text className="text-xs text-gray-500">{address}</Text>
//               </View>
//             </View>
//             <Text className="text-gray-500 mt-2 pb-4">{short_description}</Text>
//           </View>
//           <TouchableOpacity className="flex-row items-center space-x-2 p-4 border-y-2 border-gray-100 ">
//             <QuestionMarkCircleIcon color="gray" opacity={0.5} size={20} />
//             <Text className="pl-2 flex-1 text-sm font-bold">Have a food allergy ?</Text>
//             <ChevronRightIcon color="#00ccbb" />
//           </TouchableOpacity>
//         </View>
//         <View className="pb-36">
//           <Text className="px-4 pt-6 mb-3 font-bold text-xl">
//             Menu
//             {/* {DishRows} */}
//           </Text>
//           {dishes.map((dish) => (
//             <DishRow
//               key={dish._id}
//               id={dish._id}
//               name={dish.name}
//               description={dish.short_description}
//               price={dish.price}
//               image={dish.image}
//             />
//           ))}
//         </View>
//       </ScrollView>
//     </>
//   );
// };

// export default HomeScreen;
