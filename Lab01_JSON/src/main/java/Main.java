import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Main {

    public static void main(String[] args) throws JsonProcessingException{
        ObjectMapper objectMapper = new ObjectMapper();

        // object -> JSON
        User user = new User("Test",66);
        String userJson1 = objectMapper.writeValueAsString(user);

        System.out.println(userJson1);

        //JSON -> object
        String userJson2 = "{\"name\":\"John\",\"age\":21}";
        User userObject = objectMapper.readValue(userJson2,User.class);

        System.out.println(userObject.getName());
        System.out.println(userObject.getAge());
    }
}
