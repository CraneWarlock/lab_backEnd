package com.example.demo;

import org.apache.catalina.User;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;

@Controller
public class UsersController {

    private final Map<Integer, UserEntity> users= new HashMap<>(Map.of(1,new UserEntity("Tabita", 134), 2, new UserEntity("Jonasz", 10000)));

    @GetMapping("/users")
    @ResponseBody
    public Object getUsers() {
        return users;
    }

    @GetMapping ("/users/{id}/get")
    @ResponseBody
    public Object getOneUser(
            @PathVariable("id") int id
    ) {
        return users.get(id);
    }

    @GetMapping("/users/{id}/remove")
    @ResponseBody
    public Object getRemoveUsers(
            @PathVariable("id") int id
    ) {
        return users.remove(id);
    }

    @RequestMapping("/user/add")
    @ResponseBody
    public Object getAddUsers(
            @RequestParam String name,
            @RequestParam int age
    ){
        Integer id = users.size()+1;
        return users.put(id, new UserEntity(name,age));
    }
}
