local reward = 0.0
if passedCheckpoint then
    reward = (reward + 1.0)
end
if isCarReversed then
    reward = (reward - 1.0)
else
    reward = (reward + (distanceToCheckpoint * 0.03))
    reward = (reward + (speed * 0.02))
end
if (speed > 0.0) then
    reward = (reward + 0.05)
end
local count = 0
for index, value in ipairs(distanceToWall) do
    if ((count == 0) or (count == 2)) then
        if (value < 0.25) then
            reward = (reward - 1.0)
        end
    end
    if ((count == 1) or (count == 5)) then
        if (value < 0.1) then
            reward = (reward - 1.0)
        end
    end
    count = (count + 1)
    ::loop_label_1::
end
if isOutBound then
    reward = (reward - 1.0)
end
return reward